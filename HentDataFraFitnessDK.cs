using System;
using System.IO;
using System.Net;
using System.Text;

namespace FitnessDK
{
    public class HentDataFraFitnessDK
    {
        public static string GetXML(string form_build_id, DateTime dato)
        {
            var data = Encoding.ASCII.GetBytes(
                "centersearch=&" +
                "city-Aalborg=1&" +
                "centers-Aalborg%5B56%5D=56&" +
                "centers-Aalborg%5B27%5D=27&" +
                "city-Esbjerg=1&" +
                "centers-Esbjerg%5B50%5D=50&" +
                "centers-Esbjerg%5B23%5D=23&" +
                "city-Frederiksberg=1&" +
                "centers-Frederiksberg%5B15%5D=15&" +
                "centers-Frederiksberg%5B63%5D=63&" +
                "city-K%C3%B8benhavn=1&" +
                "centers-K%C3%B8benhavn%5B17%5D=17&" +
                "centers-K%C3%B8benhavn%5B54%5D=54&" +
                "centers-K%C3%B8benhavn%5B64%5D=64&" +
                "centers-K%C3%B8benhavn%5B37%5D=37&" +
                "centers-K%C3%B8benhavn%5B11%5D=11&" +
                "centers-K%C3%B8benhavn%5B61%5D=61&" +
                "centers-K%C3%B8benhavn%5B34%5D=34&" +
                "centers-K%C3%B8benhavn%5B18%5D=18&" +
                "centers-K%C3%B8benhavn%5B16%5D=16&" +
                "centers-K%C3%B8benhavn%5B22%5D=22&" +
                "centers-K%C3%B8benhavn%5B62%5D=62&" +
                "city-K%C3%B8ge=1&" +
                "centers-K%C3%B8ge%5B66%5D=66&" +
                "centers-K%C3%B8ge%5B36%5D=36&" +
                "city-Odense=1&" +
                "centers-Odense%5B55%5D=55&" +
                "centers-Odense%5B20%5D=20&" +
                "city-Viborg=1&" +
                "centers-Viborg%5B53%5D=53&" +
                "centers-Viborg%5B51%5D=51&" +
                "centers-Viborg%5B52%5D=52&" +
                "city-%C3%85rhus=1&" +
                "centers-%C3%85rhus%5B14%5D=14&" +
                "centers-%C3%85rhus%5B12%5D=12&" +
                "centers-%C3%85rhus%5B42%5D=42&" +
                "24=1&" +
                "19=1&" +
                "40=1&" +
                "69=1&" +
                "35=1&" +
                "28=1&" +
                "30=1&" +
                "29=1&" +
                "32=1&" +
                "25=1&" +
                "33=1&" +
                "39=1&" +
                "43=1&" +
                "65=1&" +
                "38=1&" +
                "41=1&" +
                "45=1&" +
                "31=1&" +
                "71=1&" +
                "68=1&" +
                "classes-search=&" +
                "instructor=&" +
                "date=" + dato.ToString("yyyy-MM-dd") + "&" +
                "form_build_id=" + form_build_id + "&" +
                "form_id=fitfe_class_booking_advanced_class_search_form&" +
                "_triggering_element_name=op&" +
                "_triggering_element_value=Show+classes&" +
                "ajax_html_ids%5B%5D=logo&" +
                "ajax_html_ids%5B%5D=search-block-form&" +
                "ajax_html_ids%5B%5D=edit-search-block-form--2&" +
                "ajax_html_ids%5B%5D=edit-actions&" +
                "ajax_html_ids%5B%5D=edit-submit--2&" +
                "ajax_html_ids%5B%5D=main&" +
                "ajax_html_ids%5B%5D=fitfe-class-booking-advanced-class-search-form&" +
                "ajax_html_ids%5B%5D=edit-filters&" +
                "ajax_html_ids%5B%5D=edit-centers&" +
                "ajax_html_ids%5B%5D=edit-centersearch&" +
                "ajax_html_ids%5B%5D=edit-list&" +
                "ajax_html_ids%5B%5D=edit-title-section-second&" +
                "ajax_html_ids%5B%5D=edit-othercenters&" +
                "ajax_html_ids%5B%5D=edit-classes-search&" +
                "ajax_html_ids%5B%5D=edit-instructor&" +
                "ajax_html_ids%5B%5D=edit-squash&" +
                "ajax_html_ids%5B%5D=edit-value&" +
                "ajax_html_ids%5B%5D=edit-submit&" +
                "ajax_html_ids%5B%5D=edit-folding&" +
                "ajax_html_ids%5B%5D=class-search-result&" +
                "ajax_html_ids%5B%5D=booking-dialog&" +
                "ajax_html_ids%5B%5D=Layer_1&" +
                "ajax_html_ids%5B%5D=Layer_1&" +
                "ajax_html_ids%5B%5D=ui-id-1&" +
                "ajax_html_ids%5B%5D=ui-id-2&" +
                "ajax_html_ids%5B%5D=&" +
                "ajax_html_ids%5B%5D=&" +
                "ajax_html_ids%5B%5D=&" +
                "ajax_html_ids%5B%5D=mm_sync_back_ground&" +
                "ajax_html_ids%5B%5D=mid-container&" +
                "ajax_page_state%5Btheme%5D=fitfe_theme&" +
                "ajax_page_state%5Btheme_token%5D=NZgwRlM6MaatjKhZjD2Pkawrf6NP9K23oLuNmHxkZl8&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fsystem%2Fsystem.base.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fsystem%2Fsystem.menus.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fsystem%2Fsystem.messages.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fsystem%2Fsystem.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.core.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.menu.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.autocomplete.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.button.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.resizable.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmisc%2Fui%2Fjquery.ui.dialog.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fdate%2Fdate_api%2Fdate.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fdate%2Fdate_popup%2Fthemes%2Fdatepicker.1.7.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Ffield%2Ftheme%2Ffield.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fnode%2Fnode.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fsearch%2Fsearch.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bmodules%2Fuser%2Fuser.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fviews%2Fcss%2Fviews.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fctools%2Fcss%2Fctools.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fpanels%2Fcss%2Fpanels.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Ftemplates%2Flayouts%2Fonecol%2F..%2F..%2F..%2Fcss%2Flayout.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Ftemplates%2Flayouts%2Fsite_template%2F..%2F..%2F..%2Fcss%2Flayout.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcomment-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcomment.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffield-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffield.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffile.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffilter.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fimage-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fimage.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Flocale-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Flocale.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fmenu.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fnode.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsearch-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsearch.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.base-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.base.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.maintenance.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.menus-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.menus.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.messages-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.messages.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.theme-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fsystem.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ftaxonomy.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fuser-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fuser.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Faddressfield-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Faddressfield.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fentity.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffield_group-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Ffield_group.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fhorizontal-tabs-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fhorizontal-tabs.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fmultipage-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fmultipage.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Flink-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Flink.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fpanels.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fpanels_dnd.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fpanels_page.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fpanelizer-ipe.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fpm_existing_pages.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fstrongarm.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fviews-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fviews.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_cart.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_checkout.base-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_checkout.base.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_checkout.theme-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_checkout.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_customer.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_line_item.theme-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_line_item.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_order.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_payment.theme-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_payment.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_price.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_product.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_tax.theme-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_tax.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_checkout_progress.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_add_to_cart_confirmation-rtl.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_add_to_cart_confirmation.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fyour_price.theme.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_flat_rate.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fcommerce_search_api.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fcss%2Fscreen.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fcss%2Fowl.carousel.css%5D=1&" +
                "ajax_page_state%5Bcss%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fcss%2Fowl.theme.css%5D=1&" +
                "ajax_page_state%5Bjs%5D%5B0%5D=1&" +
                "ajax_page_state%5Bjs%5D%5B1%5D=1&" +
                "ajax_page_state%5Bjs%5D%5B%2F%2Fajax.googleapis.com%2Fajax%2Flibs%2Fjquery%2F1.7.2%2Fjquery.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bmisc%2Fjquery.once.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bmisc%2Fdrupal.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5B%2F%2Fajax.googleapis.com%2Fajax%2Flibs%2Fjqueryui%2F1.10.2%2Fjquery-ui.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fjquery_update%2Freplace%2Fui%2Fexternal%2Fjquery.cookie.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fjquery_update%2Freplace%2Fmisc%2Fjquery.form.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bmisc%2Fstates.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bmisc%2Fajax.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fjquery_update%2Fjs%2Fjquery_update.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcontrib%2Fgss%2Fscripts%2Fautocomplete.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bpublic%3A%2F%2Flanguages%2Fda_7NLqTNrW54ZwVilxsDr74MRyBVynItdVdVFlIKfddTo.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Flibraries%2Fform2js%2Fsrc%2Fform2js.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Flibraries%2Fform2js%2Fsrc%2Fjquery.toObject.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5B%2Fprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Fbpopup.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bmisc%2Fprogress.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fcustom%2Ffitfe_class_booking_advanced%2Fjs%2Fclass_search.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fmodules%2Fglobal%2Fadapt_core%2Fmodules%2Fadapt_chaos_plugins%2Fjs%2Fcookie_information.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Fowl.carousel.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fjs%2Frespond.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fjs%2Fscript.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fglobal%2Fadapt_basetheme%2Fjs%2Fios-orientationchange-fix.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Fmodernizr.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Ffitfe-main.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Fsvg-injector.min.js%5D=1&" +
                "ajax_page_state%5Bjs%5D%5Bprofiles%2Ffitfe%2Fthemes%2Fcustom%2Ffitfe_theme%2Fjs%2Fcookie_information.js%5D=1&" +
                "ajax_page_state%5Bjquery_version%5D=1.7"
                );

            var responseContent = "";
            try
            {
                var request = WebRequest.Create("https://www.fitnessdk.dk/system/ajax");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            responseContent = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "error";
            }

            return responseContent;
        }

        public static string GetHeader()
        {
            var request = WebRequest.Create("https://www.fitnessdk.dk/class-booking-advanced");
            request.Method = "GET";

            string responseContent;

            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        responseContent = streamReader.ReadToEnd();
                    }
                }
            }
            var i = responseContent.IndexOf("Please select center wherein you want see class.");
            if (i == -1)
                i = responseContent.IndexOf("Vælg et center for at søge efter hold");
            if (i > 0)
                i = responseContent.IndexOf("name=\"form_build_id\"", i);
            if (i > 0)
            {
                var form = responseContent.Substring(i + 28, 48);
                return form;
            }
            return "";
        }
    }
}